import { configureStore, combineReducers } from "@reduxjs/toolkit";
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import modalSlice from "./reducers/modalSlice";
import storage from "redux-persist/lib/storage";
import { PersistPartial } from "redux-persist/es/persistReducer";
import authReducer from "./reducers/auth_reducers";
import notifyReducer from "./reducers/notify_reducers";
import currentReducer from "./reducers/current_reducers";

// Define your root reducer
const rootReducer = combineReducers({
  auth: authReducer,
  notify: notifyReducer,
  current: currentReducer,
  modal: modalSlice,
});

// Define your persist config
const persistConfig = {
  key: "root",
  version: 1.1,
  storage: storage,
};


// Define your persisted reducer
const persistedReducer = persistReducer(persistConfig, rootReducer);

// Define your store
export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActionPaths: ["payload.headers"],
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

export const persistor = persistStore(store);

// Get RootState and AppDispatch from store
export type RootState = ReturnType<typeof store.getState> & PersistPartial;
export type AppDispatch = typeof store.dispatch;
