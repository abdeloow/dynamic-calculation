window.initializeFileUpload = (element) => {
    // Initialize file upload if needed
    console.log('File upload initialized');
};

window.triggerFileInput = () => {
    const fileInput = document.querySelector('input[type="file"]');
    if (fileInput) {
        fileInput.click();
    }
};

window.blazorUtils = {
    // Show browser notification
    showNotification: (title, message, type) => {
        if (Notification.permission === 'granted') {
            new Notification(title, {
                body: message,
                icon: type === 'success' ? '/icons/success.png' : '/icons/error.png'
            });
        }
    },

    requestNotificationPermission: () => {
        if ('Notification' in window) {
            Notification.requestPermission();
        }
    },

    scrollToElement: (elementId) => {
        const element = document.getElementById(elementId);
        if (element) {
            element.scrollIntoView({ behavior: 'smooth' });
        }
    }
};