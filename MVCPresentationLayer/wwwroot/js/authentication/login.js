let captchaTokenField = $("#CaptchaToken");

captchaTokenField.val("");
function onCaptchaVerified(token) {
    captchaTokenField.val(token);
}