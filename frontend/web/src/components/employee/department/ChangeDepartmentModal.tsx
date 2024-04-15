import { useState } from "react";
import { useDispatch } from "react-redux";
import { UpdateDepartment } from "../../../apis/api_function";
import { useNavigate, useParams } from "react-router-dom";

const ChangeDepartmentModal = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [Loading, setLoading] = useState(false);
  const { id } = useParams();
  const { code } = useParams();
  const { name } = useParams();

  const [departmentName, setDepartmentName] = useState(name);
  const [departmentCode, setDepartmentCode] = useState(code);

  function returnToDepartment() {
    navigate("/employee/department");
  }

  async function handleSubmit(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();
    if (Loading) return;
    setLoading(true);
    if (
      id === undefined ||
      departmentCode === undefined ||
      departmentName === undefined
    ) {
      dispatch({
        type: "NOTIFY",
        payload: {
          type: "error",
          message: "Department code is undefined!",
        },
      });
      setLoading(false);
      document.getElementById("btn-close")?.click();
      return;
    }
    try {
      const res = await UpdateDepartment(
        id.toString(),
        departmentName.toString(),
        departmentCode.toString()
      );
      dispatch({
        type: "NOTIFY",
        payload: {
          type: "success",
          message: res.data.message,
        },
      });
      console.log(res);
      setLoading(false);
      returnToDepartment();
      document.getElementById("btn-close")?.click();
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (error: any) {
      console.log(error);
      dispatch({
        type: "NOTIFY",
        payload: {
          type: "error",
          message: error.response.data.message,
        },
      });
      setLoading(false);
      returnToDepartment();
      document.getElementById("btn-close")?.click();
    }
  }

  return (
    <div>
      <dialog
        id="change_department_modal"
        className="modal modal-bottom sm:modal-middle"
      >
        <div className="modal-box">
          <h3 className="font-bold text-lg">Change department</h3>
          <div className="flex flex-col gap-2 mt-4">
            <div className="flex gap-1 items-center justify-between">
              <label htmlFor="">Department Name: </label>
              <input
                type="text"
                placeholder="Department name"
                value={departmentName}
                onChange={(e) => setDepartmentName(e.target.value)}
                className="input input-bordered w-full max-w-xs"
              />
            </div>
            <div className="flex gap-1 items-center justify-between">
              <label htmlFor="">Department Code: </label>
              <input
                type="text"
                placeholder="Department code"
                value={departmentCode}
                onChange={(e) => setDepartmentCode(e.target.value)}
                className="input input-bordered w-full max-w-xs"
              />
            </div>
          </div>
          <div className="modal-action flex justify-center">
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <button
                className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
                onClick={returnToDepartment}
              >
                ✕
              </button>
            </form>
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <div className="flex justify-center gap-1">
                <button className="btn btn-error" onClick={returnToDepartment}>
                  Close
                </button>
                <button
                  className="btn bg-tim-color text-white hover:text-black"
                  onClick={(e) => handleSubmit(e)}
                >
                  Submit
                </button>
              </div>
            </form>
          </div>
        </div>
      </dialog>
    </div>
  );
};

export default ChangeDepartmentModal;
