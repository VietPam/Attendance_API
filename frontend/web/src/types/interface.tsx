//reducers
export interface IModalState {
    isOpenModal: boolean;
  }
  
  export interface IAuthState {
    _id: string;
    email: string;
    password: string;
    address: string;
    age: number;
    idNumber: string;
    gender: boolean;
    role: string;
    avatar: string;
    createAt: string;
  }
  
  export interface ICurrentState {
    dataPage: string;
  }
  