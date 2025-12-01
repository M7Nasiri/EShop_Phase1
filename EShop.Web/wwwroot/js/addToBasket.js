function addProduct(productId) {
    fetch(`/Product/Details?handler=AddToCart`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ productId: productId })
    })
        .then(r => r.json())
        .then(res => {
            if (res.guest)
                addToCart(productId);
            else
                updateCartDisplay();
        });
}
