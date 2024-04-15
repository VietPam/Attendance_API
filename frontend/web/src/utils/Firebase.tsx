// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
// import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyCELr8gUcM_Mx8bLCiDF3iu_m_wbw9UQeE",
  authDomain: "se100-emanagement.firebaseapp.com",
  projectId: "se100-emanagement",
  storageBucket: "se100-emanagement.appspot.com",
  messagingSenderId: "712406525709",
  appId: "1:712406525709:web:f2ab37a8b27bae2c753244",
  measurementId: "G-JH3FED6WH8",
};
import { getStorage } from "firebase/storage";
// Initialize Firebase
export const app = initializeApp(firebaseConfig);
// export const analytics = getAnalytics(app);
export const storage = getStorage(app);
