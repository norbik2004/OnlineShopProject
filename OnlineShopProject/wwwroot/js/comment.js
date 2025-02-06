function updateCharCount() {
    const textArea = document.getElementById("commentText");
    const charCount = document.getElementById("charCount");
    const maxLength = 150;
    const remaining = maxLength - textArea.value.length;

    console.log(remaining);

    charCount.textContent = `${remaining} characters remaining`;

    charCount.style.setProperty("color", remaining <= 10 ? "red" : "gray", "important");

}

console.log("comment.js loaded successfully!");
