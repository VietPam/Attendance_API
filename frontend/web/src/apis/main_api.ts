import axios from "axios";

export const baseURL = "https://se100-main.azurewebsites.net/api";
// export const baseURL = "http://localhost:5000/api";
export const hostURL = "https://se100-main.azurewebsites.net";

export const mainApi = axios.create({
  baseURL: baseURL,
});
