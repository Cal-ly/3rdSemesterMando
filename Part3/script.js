document.addEventListener("DOMContentLoaded", function () {
  const themeToggle = document.getElementById("themeToggle");
  const targetHeader = document.getElementById('target-header');
  const quoteElement = document.getElementById("quote");
  const newQuoteButton = document.getElementById('newQuoteButton');

  const quotes = [
    "Code is like humor. When you have to explain it, it’s bad.",
    "First, solve the problem. Then, write the code.",
    "In order to be irreplaceable, one must always be different.",
    "Simplicity is the soul of efficiency.",
    "It’s not a bug – it’s an undocumented feature.",
    "Programs must be written for people to read, and only incidentally for machines to execute.",
    "Experience is the name everyone gives to their mistakes.",
    "Code never lies, comments sometimes do.",
    "There are two ways to write error-free programs; only the third one works.",
    "The best way to predict the future is to invent it.",
    "Good code is its own best documentation.",
    "The best error message is the one that never shows up.",
    "Optimism is an occupational hazard of programming; feedback is the treatment.",
  ];

  function getRandomQuote() {
    return quotes[Math.floor(Math.random() * quotes.length)];
  }

  function toggleTheme() {
    document.body.classList.toggle("dark-theme");
    targetHeader.classList.toggle('bg-dark');
    targetHeader.classList.toggle('text-white');
  }

  function updateQuote() {
    quoteElement.textContent = getRandomQuote();
  }

  function newFunction() {
    document.getElementById('newElement').textContent = 'New Element';
  }

  themeToggle.addEventListener("click", toggleTheme);
  newQuoteButton.addEventListener('click', updateQuote);
  newQuoteButton.addEventListener('click', newFunction);

  updateQuote(); // Set initial quote
});