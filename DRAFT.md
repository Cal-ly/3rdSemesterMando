### Task 3: Modernizing the Website

The solution for Task 3 is located in the `Part3` folder, which contains an updated version of a website with the following features:

- **Responsive Design**: The website has been modernized using Bootstrap (imported via CDN) to ensure the layout adapts to different screen sizes, including mobile devices.
- **Multiple Styles**: The project includes external and internal (inline) CSS as required by the assignment.
- **Dark Theme Toggle**: A JavaScript feature allows users to toggle between light and dark themes.
- **Quote of the Day**: JavaScript dynamically loads a "Quote of the Day," which can be refreshed with a button click.

#### Project Files:

- **index.html**: The main HTML file that includes:
  - Bootstrap integration using a CDN for responsive layout.
  - External stylesheet and inline styles to meet the assignment requirements.
  - Links to various sections, including external links and internal educational content.
  - A button for toggling dark/light theme using JavaScript.
  - A section for "Quote of the Day," where JavaScript injects a random motivational quote.
  
- **style.css**: The external stylesheet containing styles for both the default and dark themes, as well as custom styles for specific elements, like:
  - `.dark-theme`: A class to toggle dark mode.
  - `.italic`: Ensures the quote is displayed in italic.
  - `.link-cadetblue`: Adds custom link styles for specific links.

- **script.js**: The JavaScript file that adds interactivity to the page, including:
  - A `Quote of the Day` feature, which pulls a random quote from a predefined list and displays it on the page.
  - A `Dark Theme` toggle, which switches between light and dark themes.
  - Event listeners to handle user interactions, such as clicking buttons to toggle the theme or get a new quote.

Example: JavaScript code to toggle themes and update quotes.

```javascript
document.addEventListener("DOMContentLoaded", function () {
  const themeToggle = document.getElementById("themeToggle");
  const quoteElement = document.getElementById("quote");
  const newQuoteButton = document.getElementById('newQuoteButton');

  const quotes = [
    "Code is like humor. When you have to explain it, it’s bad.",
    "First, solve the problem. Then, write the code."
    // Additional quotes here...
  ];

  function toggleTheme() {
    document.body.classList.toggle("dark-theme");
  }

  function updateQuote() {
    quoteElement.textContent = quotes[Math.floor(Math.random() * quotes.length)];
  }

  themeToggle.addEventListener("click", toggleTheme);
  newQuoteButton.addEventListener('click', updateQuote);

  updateQuote(); // Set initial quote
});
```