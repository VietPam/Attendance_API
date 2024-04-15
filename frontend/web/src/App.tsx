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
import CreateEmployee from './components/employee/employeeList/employeeCreate/CreateEmployee';
import Department from './components/employee/department/Department';
import EmployeeDepartment from './components/employee/department/employee/Employee'
import EmployeeProfile from './components/employee/employeeList/employeeProfile/EmployeeProfile';
import DeleteDepartmentPage from './components/employee/department/delete/DeletePage';
import ChangeDepartmentPage from './components/employee/department/change/ChangePage';
import Position from './components/position/Position';
import EmployeePosition from './components/employeePosition/EmployeePosition';
import DeletePositionPage from './components/employee/position/page/DeletePage'
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Root />}>
      <Route path="/" element={<Home />}>
        <Route index element={<Dashboard />} />
        <Route path="/" element={<Dashboard />} />
        <Route path="changepassword" element={<ChangePassword />} />
        <Route path="employee" element={<Employee />}>
          <Route path=":id" element={<EmployeeProfile />}/>
          <Route path="list" element={<EmployeeList />} />
          <Route path="create" element={<CreateEmployee />} />
          <Route path="department" element={<Department />}/>
          <Route path="department/:id/:id" element={<EmployeeDepartment />} />
          <Route path="department/delete/:id" element={<DeleteDepartmentPage />} />
          <Route path="department/change/:id/:name/:code" element={<ChangeDepartmentPage />} />
        </Route>
        <Route path="position/:name/:code" element={<Position />}/>
        <Route path="position/:name/:code/:id/:pName" element={<EmployeePosition />}/>
        <Route path="position/:name/:code/delete/:id"  element={<DeletePositionPage />} />
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
