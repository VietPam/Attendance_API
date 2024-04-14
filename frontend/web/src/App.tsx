import './App.css'
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import Root from './pages/root';
import Home from './pages/Home';
import Error from './pages/Error';
import Login from './pages/Login';
import Signup from './pages/Signup';
import Dashboard from './pages/Dashboard';
import ChangePassword from './pages/ChangePassword';
import Employee from './pages/Employee';
import EmployeeList from './components/employee/employeeList/EmployeeList';
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Root />}>
      <Route path="/" element={<Home />}>
        <Route index element={<Dashboard />} />
        <Route path="/" element={<Dashboard />} />
        <Route path="changepassword" element={<ChangePassword />} />
        <Route path="employee" element={<Employee />}>
        <Route path="list" element={<EmployeeList />} />
        <Route path="create" element={<CreateEmployee />} />

          {/* <Route path=":id" element={<EmployeeProfile />}></Route>
          <Route path="department" element={<Department />}></Route>
         
          <Route path="department/:id/:id" element={<EmployeeDepartment />} />
          <Route  
            path="department/delete/:id"
            element={<DeleteDepartmentPage />}
          />
          <Route
            path="department/change/:id/:name/:code"
            element={<ChangeDepartmentPage />}
          /> */}
        </Route>
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
