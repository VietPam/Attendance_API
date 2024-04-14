import { useEffect, useState } from "react";
import AddDepartmentModal from "./AddDepartmentModal";
import { GetDepartment } from "../../apis/api_function";
import { DepartmentRow } from "./DepartmentRow";
import { useDispatch, useSelector } from "react-redux";
// import { addDepartments } from "@/store/reducers/department_reducers";
// import Loading from "@/utils/Loading";
import React from "react";
import { RootState } from "../../store/store";

// export interface EmployeeDTO {
//   emp_ID: number;
//   avatar: string;
//   fullName: string;
//   email: string;
//   gender: boolean;
// }

// export interface PositionDTO {
//   posiition_ID: number;
//   title: string;
//   position_code: string;
//   salary_coeffcient: number;
//   employee_DTOs: EmployeeDTO[];
//   numberEmployee: number;
// }

export interface DepartmentType {
  department_ID: number;
  name: string;
  department_code: string;
  nameBoss: string;
  numberEmployee: number;
  numberPosition: number;
  // position_DTOs: PositionDTO[];
}

export interface DepartmentInforType {
  list_dep: DepartmentType[];
  current_page: number;
  perpage: number;
  pages: number;
}

const Department = () => {
  const dispatch = useDispatch();
  const listDepartment = useSelector((state: RootState) => state.department);
  const [department, setDepartment] = useState<DepartmentType[]>(
    listDepartment.list_dep || []
  );
  const [numberOfPage, setNumberOfPage] = useState(10);
  const [currentPage, setCurrentPage] = useState(
    listDepartment.current_page || 1
  );
  if (listDepartment.pages) {
    if (currentPage > listDepartment.pages) {
      setCurrentPage(listDepartment.pages);
    }
  }
  // const departmentRef = useRef<DepartmentType[]>([]);
  // const [loading, setLoading] = useState(false);

  function showModal(type: string) {
    const modal = document.getElementById(type) as HTMLDialogElement;
    if (modal !== null) {
      modal.showModal();
    }
  }

  useEffect(() => {
    const getDepartment = async () => {
      // setLoading(true);
      try {
        const res = await GetDepartment(currentPage, numberOfPage);
        console.log("currentPage", currentPage, "numberOfPage", numberOfPage);
        console.log("res.data department", res.data);
        if (res.status !== 200) {
          dispatch({
            type: "NOTIFY",
            payload: {
              type: "error",
              message: "Server error!",
            },
          });
          // setLoading(false);
          return;
        }
        const data = res.data;
        // const data = res.data.map((department: any) => {
        //   return {
        //     ...department,
        //     department_ID: BigInt(department.department_ID),
        //   };
        // });

        if (data.length === 0) {
          setCurrentPage(1);
          return;
        }

        setDepartment(data.list_dep);
        // departmentRef.current = data;
        console.log("data", data);
        dispatch({
          type: "ADD_DEPARTMENTS",
          payload: data,
        });

        // setLoading(false);
      } catch (error) {
        // dispatch({
        //   type: "NOTIFY",
        //   payload: {
        //     type: "error",
        //     message: "Server error!",
        //   },
        // });
        // setLoading(false);
        setCurrentPage(1);
        setNumberOfPage(10);
        console.log(error);
      }
    };
    // departmentRef.current = listDepartment;

    getDepartment();
  }, [dispatch, currentPage, numberOfPage]);

  // if (loading)
  //   return (
  //     <div>
  //       <Loading />
  //     </div>
  //   );
  return (
    <div className="flex flex-col gap-4 pb-60">
      <section className="flex justify-between">
        <h1 className="font-bold text-2xl text-gray-900">Departments</h1>
        <button
          className="btn bg-tim-color hover:text-black text-white"
          onClick={() => showModal("add_department_modal")}
        >
          <p>Add Departments</p>
        </button>
      </section>
      <section className="">
        <div className="overflow-x-auto bg-white border rounded-lg">
          <table className="table">
            {/* head */}
            <thead>
              <tr>
                <th>#</th>
                <th>Name</th>
                <th>Code</th>
                <th>Boss</th>
                <th>Number of Employees</th>
                <th>Number of Positions</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              {department.map((item, index) => (
                <React.Fragment key={item.department_code}>
                  <DepartmentRow item={item} itemIndex={index} />
                </React.Fragment>
              ))}
            </tbody>
          </table>
        </div>
        <div className="grid grid-cols-3 p-4">
          {/* <div className="grid grid-cols-2">
            <p>Show 1 to {numberOfPage} of 57</p>
            <div className="flex gap-2">
              <p>Rows per pages: </p>
              <select
                className="select select-bordered select-xs"
                onChange={(e) => {
                  setNumberOfPage(Number(e.target.value));
                  console.log("numberOfPage", numberOfPage);
                }}
                value={numberOfPage}
              >
                {Array.from({ length: 15 }, (_, i) => i + 1).map((value) => (
                  <option key={value} value={value}>
                    {value}
                  </option>
                ))}
              </select>
            </div>
          </div> */}
          <div className="join grid grid-cols-2 col-start-3">
            <button
              className="join-item btn btn-outline btn-sm"
              onClick={() => {
                setCurrentPage((prev) => {
                  if (prev > listDepartment.pages && listDepartment.pages)
                    return prev - 1;
                  return prev;
                });
              }}
            >
              Previous
            </button>
            <button
              className="join-item btn btn-outline btn-sm"
              onClick={() => {
                setCurrentPage((prev) => {
                  if (prev < listDepartment.pages && listDepartment.pages)
                    return prev + 1;
                  return prev;
                });
              }}
            >
              Next
            </button>
          </div>
        </div>
      </section>
      <AddDepartmentModal />
    </div>
  );
};

export default Department;
