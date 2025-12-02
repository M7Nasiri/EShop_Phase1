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

    // -----------------------------
    // 1) خواندن کوکی مهمان
    // -----------------------------
    function loadGuestCart() {
        let raw = document.cookie
            .split("; ")
            .find(x => x.startsWith("guest_cart="));
        if (!raw) return [];
        try {
            return JSON.parse(decodeURIComponent(raw.split("=")[1]));
        } catch {
            return [];
        }
    }

    // -----------------------------
    // 2) ذخیره در کوکی مهمان
    // -----------------------------
    function saveGuestCart(items) {
        const json = encodeURIComponent(JSON.stringify(items));
        document.cookie = `guest_cart=${json}; path=/; max-age=604800`; // 7 روز
    }

    // -----------------------------
    // 3) بارگذاری اولیه سبد
    // -----------------------------
    if (!isGuest && window.serverCartItems) {
        cart = window.serverCartItems.map(p => ({
            productId: p.productId,
            title: p.title,
            price: p.unitPrice,
            quantity: p.quantity
        }));

    } else {
        cart = loadGuestCart(); // فقط کوکی مهمان
    }

    updateCartDisplay();



    async function loadInitialCart() {
        if (isGuest) {
            // حالت مهمان: از کوکی می‌خوانیم
            cart = loadGuestCart();
        } else if (!isGuest && window.serverCartItems) {
            cart = window.serverCartItems.map(p => ({
                productId: p.ProductId,
                title: p.Title,
                price: p.UnitPrice,
                quantity: p.Quantity
            }));

            // پاک کردن کوکی مهمان
            document.cookie = "guest_cart=; path=/; max-age=0";
        } {
            // حالت لاگین: کوکی مهمان را merge می‌کنیم

        }

        updateCartDisplay();
    }

    // فراخوانی هنگام load
    loadInitialCart();


    // -----------------------------
    // باز شدن سبد خرید
    // -----------------------------
    basketIcon.addEventListener("click", (e) => {
        basket.classList.add("show");
        backdrop.classList.add("show");
        e.stopPropagation();
    });

    backdrop.addEventListener("click", () => {
        basket.classList.remove("show");
        backdrop.classList.remove("show");
    });

    basket.addEventListener("click", e => e.stopPropagation());

    // -----------------------------
    // افزودن محصول
    // -----------------------------
    document.querySelectorAll(".add-to-card").forEach(btn => {
        btn.addEventListener("click", async () => {
            const productId = parseInt(btn.dataset.id);

            if (isGuest) {
                let item = cart.find(x => x.productId === productId);
                if (!item) cart.push({ productId, title: btn.dataset.title, price: parseInt(btn.dataset.price), quantity: 1 });
                else item.quantity++;
                saveGuestCart(cart);
                updateCartDisplay();
            } else {
                // فقط سرور برای کاربران لاگین
                const response = await callServer("/Cart/Operation?handler=Increment", productId);
                if (response && Array.isArray(response.cartItems)) {
                    cart = response.cartItems.map(p => ({
                        productId: p.productId,
                        title: p.title,
                        price: p.unitPrice,
                        quantity: p.quantity
                    }));
                } else cart = [];
                updateCartDisplay();
            }
        });
    });

    // -----------------------------
    // تغییر تعداد یا حذف محصول (Event Delegation)
    // -----------------------------
    basketContent.addEventListener("click", async (e) => {
        const target = e.target;
        const id = parseInt(target.dataset.id);
        if (!id) return;

        if (target.classList.contains("arrow-up")) {
            if (isGuest) {
                let item = cart.find(x => x.productId === id);
                item.quantity++;
                saveGuestCart(cart);
            } else {
                const response = await callServer("/Cart/Operation?handler=Increment", id);
                if (response && Array.isArray(response.cartItems)) {
                    cart = response.cartItems.map(p => ({
                        productId: p.productId,
                        title: p.title,
                        price: p.unitPrice,
                        quantity: p.quantity
                    }));
                }
            }
        } else if (target.classList.contains("arrow-down")) {
            if (isGuest) {
                let item = cart.find(x => x.productId === id);
                item.quantity = Math.max(1, item.quantity - 1);
                saveGuestCart(cart);
            } else {
                const response = await callServer("/Cart/Operation?handler=Decrement", id);
                if (response && Array.isArray(response.cartItems)) {
                    cart = response.cartItems.map(p => ({
                        productId: p.productId,
                        title: p.title,
                        price: p.unitPrice,
                        quantity: p.quantity
                    }));
                }
            }
        } else if (target.classList.contains("delete")) {
            if (isGuest) {
                cart = cart.filter(x => x.productId !== id);
                saveGuestCart(cart);
            } else {
                const response = await callServer("/Cart/Operation?handler=Delete", id);
                if (response && Array.isArray(response.cartItems)) {
                    cart = response.cartItems.map(p => ({
                        productId: p.productId,
                        title: p.title,
                        price: p.unitPrice,
                        quantity: p.quantity
                    }));
                }
            }
        }

        updateCartDisplay();
    });

    // -----------------------------
    // حذف همه
    // -----------------------------
    bsClearBtn.addEventListener("click", async () => {
        if (isGuest) {
            cart = [];
            saveGuestCart([]);
        } else {
            const response = await callServer("/Cart/Operation?handler=ClearCart");
            cart = []; // همیشه خالی برمی‌گردد
        }
        updateCartDisplay();
    });

    // -----------------------------
    // پرداخت
    // -----------------------------
    bsPayBtn.addEventListener("click", () => {
        if (isGuest) {
            alert("لطفاً ابتدا وارد حساب کاربری شوید.");
            window.location.href = "/Login";
            return;
        }

        // حالت لاگین → انتقال به صفحه پرداخت
        window.location.href = "/Order/Checkout";
    });

    // -----------------------------
    // رندر سبد خرید
    // -----------------------------
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
                        <div class="text-muted">${Number(item.price ?? 0).toLocaleString()} تومان</div>
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
        if (bsTotalPrice)
            bsTotalPrice.textContent = `قیمت کل: ${(total || 0).toLocaleString()} تومان`;
    }

    // -----------------------------
    // ارسال درخواست به سرور
    // -----------------------------
    async function callServer(url, productId) {
        const token = document.querySelector("input[name='__RequestVerificationToken']")?.value;

        const res = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                ...(token ? { "RequestVerificationToken": token } : {})
            },
            body: JSON.stringify({ ProductId: productId })
        });

        return await res.json();
    }
});
