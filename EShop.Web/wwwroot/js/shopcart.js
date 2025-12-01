document.addEventListener("DOMContentLoaded", () => {
    let cart = [];
    const basketIcon = document.querySelector(".basket-icon");
    const basket = document.querySelector(".basket");
    const backdrop = document.querySelector(".backdrop");
    const basketContent = document.getElementById("basket-content");
    const cartCount = document.getElementById("cart-count");
    const bsTotalPrice = document.querySelector(".bs-totalPrice");
    const bsClearBtn = document.querySelector(".bs-clear-btn");
    const bsPayBtn = document.querySelector(".bs-pay-btn");

    const isGuest = !(window.userIsLoggedIn === true || window.userIsLoggedIn === "true");

    // بارگذاری اولیه
    if (!isGuest && window.serverCartItems) {
        cart = window.serverCartItems.map(p => ({
            productId: p.productId,
            title: p.title,
            price: p.unitPrice,
            quantity: p.quantity
        }));
    } else {
        const stored = localStorage.getItem("guest_cart");
        cart = stored ? JSON.parse(stored) : [];
    }

    updateCartDisplay();

    // باز و بسته شدن سبد با آیکون
    basketIcon.addEventListener("click", (e) => {
        // basket.classList.toggle("show");
        basket.classList.add("show");
        //backdrop.classList.toggle("show");
        backdrop.classList.add("show");
        e.stopPropagation();
    });

    // کلیک روی بکدراپ
    backdrop.addEventListener("click", () => {
        basket.classList.remove("show");
        backdrop.classList.remove("show");
    });

    // جلوگیری از بسته شدن وقتی روی سبد کلیک می‌کنیم
    basket.addEventListener("click", e => e.stopPropagation());

    // افزودن محصول روی دکمه
    document.querySelectorAll(".add-to-card").forEach(btn => {
        btn.addEventListener("click", () => {
            const productId = parseInt(btn.dataset.id);
            const title = btn.dataset.title;
            const price = parseInt(btn.dataset.price);

            let item = cart.find(x => x.productId === productId);
            if (!item) cart.push({ productId, title, price, quantity: 1 });
            else item.quantity++;

            if (isGuest) localStorage.setItem("guest_cart", JSON.stringify(cart));
            else callServer("/Cart/Operation?handler=Increment", productId);

            updateCartDisplay();
            basket.classList.add("show");
            backdrop.classList.add("show");
        });
    });

    // تغییر تعداد و حذف محصول داخل سبد
    basketContent.addEventListener("click", e => {
        const target = e.target;
        const id = parseInt(target.dataset.id);
        if (!id) return;

        let item = cart.find(x => x.productId === id);
        if (!item) return;

        if (target.classList.contains("arrow-up")) item.quantity++;
        else if (target.classList.contains("arrow-down")) item.quantity = Math.max(1, item.quantity - 1);
        else if (target.classList.contains("delete")) cart = cart.filter(x => x.productId !== id);

        if (isGuest) localStorage.setItem("guest_cart", JSON.stringify(cart));
        else {
            let action = target.classList.contains("arrow-up") ? "Increment" :
                target.classList.contains("arrow-down") ? "Decrement" :
                    "Delete";
            callServer(`/Cart/Operation?handler=${action}`, id);
        }

        updateCartDisplay();
    });

    // حذف همه
    bsClearBtn.addEventListener("click", () => {
        cart = [];
        if (isGuest) localStorage.removeItem("guest_cart");
        else fetch("/Cart/Operation?handler=ClearCart", { method: "POST" }).then(r => r.json());
        updateCartDisplay();
    });

    // پرداخت
    bsPayBtn.addEventListener("click", () => {
        window.location.href = "/User/Checkout";
    });

    // بروزرسانی نمایش سبد
    function updateCartDisplay() {
        basketContent.innerHTML = "";
        let total = 0;

        cart.forEach(item => {
            const qty = item.quantity ?? 1;
            const itemTotal = (item.price ?? 0) * qty;
            total += itemTotal;

            basketContent.innerHTML += `
                <div class="basket-item" data-id="${item.productId}">
                    <div>
                        <strong>${item.title}</strong>
<div class="text-muted">${Number(item.price ?? item.unitPrice ?? 0).toLocaleString()} تومان</div>
                    </div>
                    <div class="d-flex align-items-center gap-2">
                        <button class="btn btn-sm btn-outline-secondary arrow-up" data-id="${item.productId}">+</button>
                        <span>${qty}</span>
                        <button class="btn btn-sm btn-outline-secondary arrow-down" data-id="${item.productId}">-</button>
                        <button class="btn btn-sm btn-danger delete" data-id="${item.productId}">X</button>
                    </div>
                    <div>${itemTotal.toLocaleString()} تومان</div>
                </div>`;
        });

        cartCount.textContent = cart.length;
        if (bsTotalPrice) bsTotalPrice.textContent = `قیمت کل: ${(total || 0).toLocaleString()} تومان`;
    }

    // فراخوانی سرور
    async function callServer(url, productId) {
        const token = document.querySelector("input[name='__RequestVerificationToken']")?.value;
        await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json", ...(token ? { "RequestVerificationToken": token } : {}) },
            body: JSON.stringify({ productId })
        });
    }
});
