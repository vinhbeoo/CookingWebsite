// Lắng nghe sự kiện click trên biểu tượng chuông
notificationIcon.addEventListener("click", function (event) {
    event.stopPropagation();

    // Gọi API để lấy thông tin mới nhất về thông báo
    fetch("https://localhost:7269/api/Notification")
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            // Cập nhật giao diện dropdown menu thông báo với thông tin mới nhất
            var notificationsList = notificationsDropdown.querySelector(".notifications-list");
            notificationsList.innerHTML = ""; // Xóa thông báo cũ

            data.forEach(function (notification) {
                var notificationItem = document.createElement("li");
                notificationItem.textContent = notification.message;
                notificationsList.appendChild(notificationItem);
            });

            // Cập nhật số thông báo khi hiển thị dropdown
            var badgeNumber = document.querySelector(".badge-number");
            badgeNumber.textContent = data.length.toString();
        })
        .catch(function (error) {
            console.error("Error fetching notifications:", error);
        });

    notificationsDropdown.classList.toggle("show-notifications");
});