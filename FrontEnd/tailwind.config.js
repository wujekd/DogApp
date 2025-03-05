/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        secondary: "#2997FF",
        primary: {
          10: "#06402B",
          100: "#94928d",
          200: "#afafaf",
          300: "#42424570",
        },
        accent: "#93257f",
        text: "#d2d0e5",
        background: "#0c0808"
      },
    },
  },
  plugins: [],
};