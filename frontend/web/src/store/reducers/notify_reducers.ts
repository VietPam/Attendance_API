import { createReducer, createAction } from "@reduxjs/toolkit";

interface NotifyState {
    type: string;
    message: string;
  }
  
  // Initial State
  const initialState = {
    type: "",
    message: "",
  } as NotifyState;
  
  // Actions
  export const notify = createAction<NotifyState>("NOTIFY");
  export const unnotify = createAction("UN_NOTIFY");
  
  // Reducer
  const notifyReducer = createReducer(initialState, (builder) => {
    builder
      .addCase(notify, (state, action) => {
        state.type = action.payload.type;
        state.message = action.payload.message;
      })
      .addCase(unnotify, (state) => {
        state.type = "";
        state.message = "";
      });
  });
  
  export default notifyReducer;
  