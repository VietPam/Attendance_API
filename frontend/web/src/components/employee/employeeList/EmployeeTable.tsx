import {
    // MdOutlineDeleteForever,
    // MdOutlineEdit,
    MdMailOutline,
  } from "react-icons/md";
  import { openModal } from "../../../store/reducers/modalSlice";
  import { useDispatch } from "react-redux";
  import {
    CONFIRMATION_MODAL_CLOSE_TYPES,
    MODAL_BODY_TYPES,
  } from "../../../utils/globalConstantUtil";
  // import { useNavigate } from "react-router-dom";
  
  import { EmployeeProps } from "./EmployeeList";
  
  interface EmployeeTableProps {
    employee: EmployeeProps[];
  }
  
  const EmployeeTable = ({ employee }: EmployeeTableProps) => {
    // const navigate = useNavigate();
    const dispatch = useDispatch();
  
    const sendMailToEmployee = (id: number) => {
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
  
    return (
      <section className="bg-white border rounded-lg">
        <div className="overflow-x-auto">
          <table className="table">
            {/* head */}
            <thead>
              <tr>
                <th>#</th>
                <th>Avatar</th>
                <th>Email</th>
                <th>Name</th>
                <th>Phone</th>
                <th>Birth Day</th>
                <th>Gender</th>
                <th>ID Number</th>
                <th>Address</th>
              </tr>
            </thead>
            <tbody>
              {/* rows */}
              {employee.map((item, index) => (
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
                  <td className="max-w-[10rem] break-normal">{item.fullName}</td>
                  <td>{item.phoneNumber}</td>
                  <td className="min-w-[7rem]">{`${new Date(
                    item.birth_day
                  ).getDate()}-${
                    new Date(item.birth_day).getMonth() + 1
                  }-${new Date(item.birth_day).getFullYear()}`}</td>
                  <td>{item.gender ? "Male" : "Female"}</td>
                  <td>{item.cmnd}</td>
                  <td className="max-w-[14rem] break-words">{item.address}</td>
  
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
                      // onClick={() => showModal("delete_profile_modal")}
                    >
                      <MdOutlineDeleteForever className="h-5 w-5" />
                    </button> */}
                  </th>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>
    );
  };
  
  export default EmployeeTable;
  