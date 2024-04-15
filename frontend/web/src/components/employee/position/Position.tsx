/* eslint-disable @typescript-eslint/no-unused-vars */
import AddPositionModal from "./AddPositionModal";
import ChangePositionModal from "./ChangePositionModal";
import { Navigate, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import { GetPositionByDepartmentCode } from "../../../apis/api_function";
import { PositionRow } from "./PositionRow";
import { MdKeyboardBackspace } from "react-icons/md";
import { useNavigate } from "react-router-dom";
// import { useSelector } from "react-redux";
// import { RootState } from "@/store/store";

export interface PositionDTO {
  id: number;
  title: string;
  code: string;
  salary_coeffcient: number;
  // employee_DTOs: EmployeeDTO[];
  emp_count: number;
}

const positionArray = [
  {
    id: 1,
    title: "...",
    code: "...",
    salary_coeffcient: 0,
    // employee_DTOs: [],
    emp_count: 0,
  },
];

const Position = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const departmentCode = location.pathname.split("/")[3];
  const name = location.pathname.split("/")[2];
  // const departments = useSelector(
  //   (state: RootState) => state.department.listDepartment
  // );
  const departmentName = decodeURIComponent(name.replace(/%20/g, " "));

  const [positions, setPositions] = useState<PositionDTO[]>(positionArray);
  const numberOfPage = 10;
  const [currentPage, setCurrentPage] = useState(1);

  function showModal(type: string) {
    const modal = document.getElementById(type) as HTMLDialogElement;
    if (modal !== null) {
      modal.showModal();
    }
  }

  useEffect(() => {
    async function getPosition() {
      const res = await GetPositionByDepartmentCode(
        departmentCode,
        currentPage,
        numberOfPage
      );

      const data = res.data;
      if (data.length === 0) {
        setCurrentPage(1);
        return;
      }

      setPositions(data);
    }
    getPosition();
  }, [positionArray, departmentCode, currentPage, numberOfPage]);

  if (name === "...") return <Navigate to="/employee/department" />;
  return (
    <div className="flex flex-col gap-4">
      <div className="flex justify-start">
        <button
          onClick={() => {
            navigate("/employee/department");
          }}
          className="flex btn btn-link items-center gap-2"
        >
          <MdKeyboardBackspace />
          <p>Back</p>
        </button>
      </div>
      <section className="flex justify-between mt-4">
        <h1 className="font-bold text-2xl text-gray-900 flex gap-2">
          <p className="font-bold text-2xl ">Positions in</p>
          <p className="font-bold text-2xl">{departmentName}</p>
        </h1>
        <button
          className="btn bg-tim-color hover:text-black text-white"
          onClick={() => showModal("add_position_modal")}
        >
          <p>Add Positions</p>
        </button>
      </section>
      <section className="">
        <div className="overflow-x-auto bg-white border rounded-lg">
          <table className="table">
            {/* head */}
            <thead>
              <tr>
                <th>#</th>
                <th>Code</th>
                <th>Title</th>
                <th>Number of Employee</th>
                <th>Coefficient</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              {/* rows */}
              {positions?.map((item, index) => (
                <PositionRow key={item.id} item={item} index={index} />
              ))}
            </tbody>
          </table>
        </div>
        <div className="grid grid-cols-3 p-4">
          <div className="join grid grid-cols-2 col-start-3">
            <button
              className="join-item btn btn-outline btn-sm"
              onClick={() => {
                setCurrentPage((prev) => {
                  return prev - 1;
                });
              }}
            >
              Previous
            </button>
            <button
              className="join-item btn btn-outline btn-sm"
              onClick={() => {
                setCurrentPage((prev) => {
                  return prev + 1;
                });
              }}
            >
              Next
            </button>
          </div>
        </div>
      </section>
      <AddPositionModal departmentId={departmentCode} />
      <ChangePositionModal />
    </div>
  );
};

export default Position;
