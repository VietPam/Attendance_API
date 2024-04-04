import './App.css'
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import Root from './pages/root';
import Home from './pages/Home';
import Error from'./pages/Error';
import Login from './pages/Login';
import Signup from './pages/Signup';
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Root />}>
      <Route path="/" element={<Home />}>
        
      </Route>
      <Route path="login" element={<Login />} />
      <Route path="signup" element={<Signup />} />

      <Route path="*" element={<Error />} />
    </Route>
  )
);
function App() {
  return (
    <div className="App">
      <RouterProvider router={router} />
    </div>
  );
}
export default App
