/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./src/**/*.{js,ts,jsx,tsx}",
    "./node_modules/react-tailwindcss-datepicker/dist/index.esm.js",
  ],
  theme: {
    extend: {
      colors: {
        "tim-color": "#484C7F",
        // #484c7f
        "tim-color-1": "#353966",
        "primary-1": "#00CCCD",
        "secondary-0": "#FFC107",
        "do-color": "#DC3545",
        "secondary-2": "#198754",
        "secondary-3": "#0D6EFD",
      },
      screens: {
        phone: "444px",
        // => @media (min-width: 640px) { ... }

        "phone-1": "516px",
        // => @media (min-width: 640px) { ... }

        tablet: "640px",
        // => @media (min-width: 640px) { ... }

        laptop: "1024px",
        // => @media (min-width: 1024px) { ... }

        desktop: "1280px",
        // => @media (min-width: 1280px) { ... }
      },
    },
  },
  // eslint-disable-next-line no-undef
  plugins: [require("@tailwindcss/typography"), require("daisyui")],

  // daisyUI config (optional - here are the default values)
  daisyui: {
    themes: ["light", "lofi"], // true: all themes | false: only light + dark | array: specific themes like this ["light", "dark", "cupcake"]
    darkTheme: "lofi", // name of one of the included themes for dark mode
  },
};
