import {
    ref,
    uploadBytes,
    deleteObject,
    getDownloadURL,
  } from "firebase/storage";
  import { storage } from "../utils/Firebase";
  
  export function uploadFirebaseImage(image: File): Promise<string> {
    const storageRef = ref(storage, image.name.toString());
  
    return new Promise((resolve, reject) => {
      uploadBytes(storageRef, image)
        .then(() => {
          getDownloadURL(storageRef)
            .then((downloadURL) => {
              console.log("File available at", downloadURL);
              resolve(downloadURL);
            })
            .catch((error) => {
              console.error("Error getting download URL: ", error);
              reject(error);
            });
        })
        .catch((error) => {
          console.error("Error uploading file: ", error);
          reject(error);
        });
    });
  }
  
  export function deleteFirebaseImage(imageUrl: string): Promise<void> {
    return new Promise((resolve, reject) => {
      const imageRef = ref(storage, imageUrl);
      deleteObject(imageRef)
        .then(() => {
          console.log("File deleted successfully");
          resolve();
        })
        .catch((error) => {
          console.error("Error deleting file: ", error);
          reject(error);
        });
    });
  }
  