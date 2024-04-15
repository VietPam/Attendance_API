import { DeleteDepartment } from "../../../apis/api_function";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useParams, useNavigate } from "react-router-dom";

const DeleteDepartmentModal = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();
  const [Loading, setLoading] = useState(false);
  const code = id?.toString();

  function returnToDepartment() {
    navigate("/employee/department");
  }

  async function handleSubmitDelete() {
    if (Loading) return;
    setLoading(true);
    try {
      console.log("codeDel", code);
      if (code === undefined) {
        dispatch({
          type: "NOTIFY",
          payload: {
            type: "error",
            message: "Department code is undefined!",
          },
        });
        setLoading(false);
        document.getElementById("btn-close")?.click();
        returnToDepartment();
        return;
      }
      const res = await DeleteDepartment(code);
      dispatch({
        type: "NOTIFY",
        payload: {
          type: "success",
          message: res.data.message,
        },
      });
      console.log(res);
      setLoading(false);
      // window.location.reload();
      document.getElementById("btn-close")?.click();
      returnToDepartment();
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
      // window.location.reload();
      document.getElementById("btn-close")?.click();
      returnToDepartment();
    }
  }

  return (
    <div>
      <dialog
        id="delete_department_modal"
        className="modal modal-bottom sm:modal-middle"
      >
        <div className="modal-box">
          <h3 className="font-bold text-lg">Delete Department</h3>
          <div>
            <h3>Are you sure?</h3>
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
                <button className="btn" onClick={returnToDepartment}>
                  No
                </button>
                <button
                  className="btn btn-error text-white hover:text-black"
                  onClick={handleSubmitDelete}
                >
                  Yes
                </button>
              </div>
            </form>
          </div>
        </div>
      </dialog>
    </div>
  );
};

export default DeleteDepartmentModal;
