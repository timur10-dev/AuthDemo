const messageElement = document.getElementById("welcomeText");

const user = sessionStorage.getItem("username");

messageElement.textContent = `Welcome, ${user}!`;

const music = document.getElementById("music");
music.volume = 0.3;