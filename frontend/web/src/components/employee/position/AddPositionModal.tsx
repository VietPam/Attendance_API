import { GetDepartment, NewPosition } from "../../../apis/api_function";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useDispatch } from "react-redux";
import { useState } from "react";

interface FormValues {
  title: string;
  code: string;
  coefficient: number;
}

interface Props {
  departmentId: string;
}

const schema = yup.object().shape({
  title: yup.string().required(),
  code: yup.string().required(),
  coefficient: yup.number().required(),
});

const PositionModal = ({ departmentId }: Props) => {
  const dispatch = useDispatch();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    mode: "onBlur",
    resolver: yupResolver(schema),
  });

  const [Loading, setLoading] = useState(false);

  async function Done(data: FormValues) {
    if (Loading) return;
    setLoading(true);
    try {
      const res = await NewPosition(
        data.title,
        data.code,
        data.coefficient,
        departmentId
      );
      if (res.status === 200) {
        setLoading(false);
        dispatch({ type: "SET_SUCCESS", payload: "Add position success" });

        async function getDepartment() {
          const res = await GetDepartment(1, 20);
          const data = res.data;
          console.log("GetDepartment", data);
          dispatch({
            type: "ADD_DEPARTMENTS",
            payload: { listDepartment: data },
          });
        }
        getDepartment();

        // window.location.reload();
        document.getElementById("btn-close")?.click();
      }
    } catch (error) {
      dispatch({ type: "NOTIFY", payload: error });
      console.log(error);
      setLoading(false);
      window.location.reload();
      document.getElementById("btn-close")?.click();
    }
  }

  return (
    <div>
      {/* Open the modal using document.getElementById('ID').showModal() method */}
      <dialog
        id="add_position_modal"
        className="modal modal-bottom sm:modal-middle"
      >
        <div className="modal-box">
          <h3 className="font-bold text-lg">Add position</h3>
          <div className="flex flex-col gap-2 mt-4">
            <div className="flex gap-1 items-center justify-between">
              <label htmlFor="">Position Title: </label>
              <input
                type="text"
                placeholder="Position Name"
                className="input input-bordered w-full max-w-xs"
                {...register("title")}
              />
            </div>
            {errors.title && (
              <span className="text-red-600">{errors.title.message}</span>
            )}
            <div className="flex gap-1 items-center justify-between">
              <label htmlFor="">Position Code: </label>
              <input
                type="text"
                placeholder="Position Code"
                className="input input-bordered w-full max-w-xs"
                {...register("code")}
              />
            </div>
            {errors.code && (
              <span className="text-red-600">{errors.code.message}</span>
            )}
            <div className="flex gap-1 items-center justify-between">
              <label htmlFor="">Position Coefficient: </label>
              <input
                type="number"
                placeholder="Position Coefficient"
                className="input input-bordered w-full max-w-xs"
                {...register("coefficient")}
              />
            </div>
            {errors.coefficient && (
              <span className="text-red-600">{errors.coefficient.message}</span>
            )}
          </div>
          <div className="modal-action flex justify-center">
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
                ✕
              </button>
            </form>
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <div className="flex justify-center gap-1">
                <button id="btn-close" className="btn btn-error">
                  Close
                </button>
                <button
                  className="btn bg-tim-color text-white hover:text-black"
                  onClick={handleSubmit(Done)}
                  disabled={Loading}
                >
                  {Loading && (
                    <span className="loading loading-infinity loading-md"></span>
                  )}
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

export default PositionModal;
