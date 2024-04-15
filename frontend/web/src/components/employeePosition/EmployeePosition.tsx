import { useEffect, useState } from "react";
import { GetEmployeeByPositionId } from "../../apis/api_function";
import { useDispatch } from "react-redux";
import React from "react";
// import { RootState } from "@/store/store";
import { useParams, useNavigate } from "react-router-dom";
import {
  // MdOutlineDeleteForever,
  // MdOutlineEdit,
  MdMailOutline,
  MdKeyboardBackspace,
} from "react-icons/md";
import { openModal } from "../../store/reducers/modalSlice";
import {
  CONFIRMATION_MODAL_CLOSE_TYPES,
  MODAL_BODY_TYPES,
} from "../../utils/globalConstantUtil";

// "id": 999999999,
//       "email": "admin@gmail.com",
//       "fullName": "admin",
//       "phoneNumber": "string",
//       "avatar": "string",
//       "birth_day": "2023-12-26T11:50:03.9484762Z",
//       "gender": true,
//       "cmnd": "string",
//       "address": "string"

export interface EmployeeType {
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

export interface EmployeeInforType {
  list_emp: EmployeeType[];
  current_page: number;
  perpage: number;
  pages: number;
}

const EmployeePosition = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { id, pName } = useParams();
  // const listEmployee = useSelector((state: RootState) => state.employee);
  const [employee, setEmployee] = useState<EmployeeType[]>([]);
  const [numberOfPage, setNumberOfPage] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);
  // if (listEmployee.pages) {
  //   if (currentPage > listEmployee.pages) {
  //     setCurrentPage(listEmployee.pages);
  //   }
  // }
  // const employeeRef = useRef<EmployeeType[]>([]);
  // const [loading, setLoading] = useState(false);

  // function showModal(type: string) {
  //   const modal = document.getElementById(type) as HTMLDialogElement;
  //   if (modal !== null) {
  //     modal.showModal();
  //   }
  // }

  useEffect(() => {
    const getEmployee = async () => {
      // setLoading(true);
      try {
        const res = await GetEmployeeByPositionId(id || "1", 1, 10);
        console.log("res.data employee", res.data);
        if (res.data === 0) {
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
        if (data.length === 0) {
          setCurrentPage(1);
          return;
        }
        setEmployee(data.list_emp);
        // employeeRef.current = data;
        console.log("setEmployee", employee);
        dispatch({
          type: "ADD_EMPLOYEES",
          payload: data,
        });

        // setLoading(false);
      } catch (error) {
        dispatch({
          type: "NOTIFY",
          payload: {
            type: "error",
            message: "Server error!",
          },
        });
        // setLoading(false);
        setCurrentPage(1);
        setNumberOfPage(10);
        console.log(error);
      }
    };
    // employeeRef.current = listEmployee;

    getEmployee();
  }, [dispatch, currentPage, numberOfPage]);

  const sendMailToEmployee = (id: number) => {
    console.log("id", id);
    dispatch(
      openModal({
        title: "Send mail to employee",
        bodyType: MODAL_BODY_TYPES.CONFIRMATION,
        extraObject: {
          message: `Do you want to send reset password email to this employee?`,
          type: CONFIRMATION_MODAL_CLOSE_TYPES.EMAIL_SEND,
          _id: id,
        },
      })
    );
  };

  // if (loading)
  //   return (
  //     <div>
  //       <Loading />
  //     </div>
  //   );
  return (
    <div className="flex flex-col gap-4">
      <div className="flex justify-start">
        <button
          onClick={() => {
            navigate(-1);
          }}
          className="flex btn btn-link items-center gap-2"
        >
          <MdKeyboardBackspace />
          <p>Back</p>
        </button>
      </div>
      <section className="flex justify-between">
        <h1 className="font-bold text-2xl text-gray-900">
          Employee in {pName}
        </h1>
        <button
          className="btn bg-tim-color hover:text-black text-white"
          onClick={() => navigate("/employee/create")}
        >
          <p>Add Employees</p>
        </button>
      </section>
      <section className="">
        <div className="overflow-x-auto bg-white border rounded-lg">
          <table className="table">
            {/* head */}
            <thead>
              <tr>
                <th>#</th>
                <th>Image</th>
                <th>Mail</th>
                <th>Name</th>
                <th>Phone</th>
                <th>Birth day</th>
                <th>Gender</th>
                <th>Id</th>
                <th>Address</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              {employee.map((item, index) => (
                <React.Fragment key={item?.ID}>
                  <tr key={item.ID}>
                    <td>{index + 1}</td>
                    <td>
                      <div className="avatar">
                        <div className="w-12 rounded-full">
                          <img
                            src={
                              item.avatar && item.avatar !== "string"
                                ? item.avatar
                                : "https://t4.ftcdn.net/jpg/02/29/75/83/360_F_229758328_7x8jwCwjtBMmC6rgFzLFhZoEpLobB6L8.jpg"
                            }
                            alt="avatar"
                          />
                        </div>
                      </div>
                    </td>
                    <td className="max-w-[16rem] break-normal">{item.email}</td>
                    <td className="max-w-[10rem] break-normal">
                      {item.fullName}
                    </td>
                    <td>{item.phoneNumber}</td>
                    <td>{`${new Date(item.birth_day).getDate()}-${
                      new Date(item.birth_day).getMonth() + 1
                    }-${new Date(item.birth_day).getFullYear()}`}</td>
                    <td>{item.gender ? "Male" : "Female"}</td>
                    <td>{item.cmnd}</td>
                    <td className="max-w-[14rem] break-words">
                      {item.address}
                    </td>

                    <th className="flex gap-1">
                      <button
                        className="btn btn-ghost btn-xs border text-green-800 border-green-800"
                        onClick={() =>
                          sendMailToEmployee(item.id ? item.id : item.ID)
                        }
                      >
                        <MdMailOutline className="h-5 w-5" />
                      </button>
                      {/* <button
                        className="btn btn-ghost btn-xs border text-tim-color border-tim-color-1"
                        key={item.id}
                        // onClick={() => navigate(`/employee/${item.id}`)}
                      >
                        <MdOutlineEdit className="h-5 w-5" />
                      </button> */}
                      {/* <button
                        className="btn btn-ghost btn-xs text-red-600 border border-red-600"
                        key={item.id}
                        // onClick={() => showModal("delete_profile_modal")}
                      >
                        <MdOutlineDeleteForever className="h-5 w-5" />
                      </button> */}
                    </th>
                  </tr>
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
            {/* <button
              className="join-item btn btn-outline btn-sm"
              onClick={() => {
                setCurrentPage((prev) => {
                  if (prev > listEmployee.pages && listEmployee.pages)
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
                  if (prev < listEmployee.pages && listEmployee.pages)
                    return prev + 1;
                  return prev;
                });
              }}
            >
              Next
            </button> */}
          </div>
        </div>
      </section>
    </div>
  );
};

export default EmployeePosition;
