document.getElementById("CaptchaToken").value = "";
function onCaptchaVerified(token) {
    document.getElementById("CaptchaToken").value = token;
}