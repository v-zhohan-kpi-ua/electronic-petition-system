const { fontFamily } = require("tailwindcss/defaultTheme");
const colors = require("tailwindcss/colors");

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{ts,tsx}", "./public/**/*.html"],
  plugins: [require("@tailwindcss/forms")],
  theme: {
    extend: {
      animation: {
        'spin-fast': 'spin 0.5s linear infinite',
      },
      fontFamily: {
        sans: ["var(--font-roboto)", ...fontFamily.sans],
      },
      colors: {
        primary: {
          50: "#f6f8f3",
          100: "#dbe3d1",
          200: "#bccbaa",
          300: "#97ad7a",
          400: "#829d60",
          500: "#66873c",
          600: "#4e741e",
          700: "#3e5d17",
          800: "#354f13",
          900: "#26390e",
        },
        emerald: {
          50: "#F3FAF7",
          100: "#DEF7EC",
          200: "#BCF0DA",
          300: "#84E1BC",
          400: "#31C48D",
          500: "#0E9F6E",
          600: "#057A55",
          700: "#046C4E",
          800: "#03543F",
          900: "#014737",
        },
      },
    },
  },
};
