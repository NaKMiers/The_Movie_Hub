/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./**/*.{cshtml,css,js}'],
  theme: {
    extend: {
      colors: {
        'primary-tw': '#639',
        'secondary-tw': '#0f172a',
        highlight: '#f3ea28',
        dark: '#121212',
        light: '#fff',
        success: '#07bc0c',
        info: '#3498db',
        warning: '#f1c40f',
        error: '#e74c3c',
      },
      screens: {
        below601: { max: '600px' },
      },
      fontFamily: {
        sans: ['Josefin Sans', 'sans-serif'],
        body: ['Anton', 'sans-serif'],
      },
      spacing: {
        21: '21px',
        '21/2': '10.5px',
      },
      maxWidth: {
        1200: '1200px',
      },
      borderRadius: {
        large: '24px',
        medium: '16px',
        small: '12px',
        'extra-small': '6px',
      },
      backgroundColor: {
        dark: {
          100: '#2f2e3e',
          200: '#003e70',
        },
        light: {
          100: '#f4f4f4',
          200: '#fff',
        },
      },
      backgroundImage: {
        'gradient-linear': 'linear-gradient(106deg, #639, #36c 102.69%)',
      },
      textColor: {
        light: '#fff',
        darker: '#003e70',
        dark: '#333',
      },
      boxShadow: {
        'medium-light': '0px 2px 10px 1px rgba(255, 255, 255, 0.1)',
        medium: '0px 14px 10px 5px rgba(0, 0, 0, 0.2)',
        small: '0px 2px 10px 1px rgba(0, 0, 0, 0.1)',
      },
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
        'gradient-conic': 'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))',
      },
      fill: {
        light: '#fff',
      },
      keyframes: {
        wiggle: {
          '0%, 100%': { transform: 'rotate(-3deg)' },
          '50%': { transform: 'rotate(3deg)' },
        },
        'scale-wiggle': {
          '0%, 100%': { transform: 'rotate(-3deg) scale(1.2)' },
          '50%': { transform: 'rotate(3deg) scale(1.2)' },
        },
      },
      animation: {
        'spin-slow': 'spin 2s linear infinite',
        wiggle: 'wiggle 0.8s ease-in-out infinite',
        'scale-wiggle': 'scale-wiggle 0.8s ease-in-out infinite 0.2s',
      },
      screens: {
        sm: '640px',

        md: '768px',

        mx: '900px',

        lg: '1024px',

        xl: '1200px',

        '2xl': '1536px',
      },
    },
  },
  plugins: [],
}
