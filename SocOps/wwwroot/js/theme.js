window.themeInterop = {
    getStoredTheme: () => localStorage.getItem("theme"),
    setTheme: (theme) => {
        localStorage.setItem("theme", theme);
        document.documentElement.setAttribute("data-theme", theme);
    }
};
