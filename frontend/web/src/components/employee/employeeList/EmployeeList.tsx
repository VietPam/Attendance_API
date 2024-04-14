import { useNavigate } from "react-router-dom";
import DeleteProfile from "./DeleteProfile";
// import { AddEmployeeExcel } from "./AddEmployeeExcel";
import { useState, useEffect, SetStateAction } from "react";
import {
  GetDepartment,
  GetEmployeeByDepartmentCode,
  GetPositionByDepartmentCode,
  GetEmployeeByPositionId,
} from "../../../apis/api_function";
import { DepartmentType } from "../../department/Department";
import { PositionDTO } from "../../position/Position";
import EmployeeTable from "./EmployeeTable";
import Loading from "../../../utils/Loading";

export interface EmployeeProps {
  ID: number;
  id: number;
  email: string;
  fullName: string;
  phoneNumber: string;
  avatar: string;
  birth_day: string;
  gender: boolean;
  cmnd: string;
  address: string;
}

const EmployeeList = () => {
  const navigate = useNavigate();
  const [employees, setEmployees] = useState<EmployeeProps[]>([]);
  const [department, setDepartment] = useState<DepartmentType[]>([]);
  const [position, setPosition] = useState<PositionDTO[]>([]);
  const [currentDepartment, setCurrentDepartment] = useState("");
  const [currentPosition, setCurrentPosition] = useState("");
  const [isPosition, setIsPosition] = useState(false);
  const [loading, setLoading] = useState(false);

  // function showModal(type: string) {
  //   const modal = document.getElementById(type) as HTMLDialogElement;
  //   if (modal !== null) {
  //     modal.showModal();
  //   }
  // }

  function getCurrentDepartment(event: {
    target: { value: SetStateAction<string> };
  }) {
    setIsPosition(false);
    setCurrentDepartment(event.target.value);
  }

  function getCurrentPosition(event: {
    target: { value: SetStateAction<string> };
  }) {
    setIsPosition(true);
    setCurrentPosition(event.target.value);
  }

  async function GetPositionByDepartment(dep: string) {
    try {
      setLoading(true);
      const res = await GetPositionByDepartmentCode(dep, 1, 20);
      setPosition(res.data);
      console.log("res.data position", res.data);
      setLoading(false);
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    async function getDepartment() {
      try {
        const res = await GetDepartment(1, 100);
        setDepartment(res.data.list_dep);
      } catch (error) {
        console.log(error);
      }
    }
    getDepartment();
  }, [currentPosition]);

  useEffect(() => {
    async function getEmployee() {
      setLoading(true);
      try {
        if (isPosition) {
          const res = await GetEmployeeByPositionId(currentPosition, 1, 100);
          setEmployees(res.data.list_emp);
          return;
        }
        const res = await GetEmployeeByDepartmentCode(currentDepartment);
        setEmployees(res.data.list_emp);
      } catch (error) {
        console.log(error);
      }
      setLoading(false);
    }
    if (department.length === 0 || !department) {
      // console.log("th1", department);
      return;
    } else if (currentDepartment === "" && department.length > 0) {
      // console.log("th2", department);
      setCurrentDepartment(department[0].department_code);
    } else if (currentDepartment !== "" && department.length > 0) {
      GetPositionByDepartment(currentDepartment);
      getEmployee();
    }
  }, [currentDepartment, currentPosition, department, isPosition]);

  return (
    <div className="flex flex-col gap-4">
      <section className="flex justify-between">
        <h1 className="font-bold text-2xl text-gray-900">Employees</h1>

        <div className="flex justify-center">
          <button
            className="btn bg-tim-color hover:text-black text-white"
            onClick={() => navigate("/employee/create")}
          >
            <p>Add Employees</p>
          </button>
          {/* <AddEmployeeExcel /> */}
        </div>
      </section>
      <section>
        <div className="flex justify-start items-center ">
          <p>Filter by:</p>
          <div className="mx-2 flex gap-2">
            <select
              id="department"
              className="select select-bordered w-full max-w-sm"
              onChange={getCurrentDepartment}
            >
              {/* <option disabled selected>
                Department
              </option> */}
              {department?.map((item: DepartmentType) => (
                <option
                  key={item.department_code}
                  value={item.department_code}
                  className="w-full"
                >
                  {item.name}
                </option>
              ))}
            </select>
            <select
              id="position"
              className="select select-bordered w-full max-w-xs"
              onChange={getCurrentPosition}
            >
              <option disabled>Position</option>
              {position?.map((item: PositionDTO) => (
                <option key={item.id} value={item.id}>
                  {item.title}
                </option>
              ))}
            </select>
          </div>
        </div>
      </section>
      {loading && <Loading />}
      {!loading && employees.length === 0 ? (
        <div className="flex justify-center items-center">
          <p className="text-gray-400">
            {isPosition
              ? "There is no employee in this position"
              : "There is no employee in this department"}
          </p>
        </div>
      ) : (
        <EmployeeTable employee={employees} />
      )}

      <DeleteProfile />
    </div>
  );
};

export default EmployeeList;
