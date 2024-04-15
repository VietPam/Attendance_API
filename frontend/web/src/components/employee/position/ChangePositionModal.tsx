import { UpdatePosition } from "../../../apis/api_function";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useParams, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";

interface FormValues {
  title: string;
  code: string;
  coefficient: number;
}

const schema = yup.object().shape({
  title: yup.string().required(),
  code: yup.string().required(),
  coefficient: yup.number().required(),
});

const PositionModal = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    mode: "onBlur",
    resolver: yupResolver(schema),
  });
  const { id, title, code, coefficient } = useParams();
  const [Loading, setLoading] = useState(false);
  const _id = id?.toString();
  const _title = title?.toString();
  const _code = code?.toString();
  const _coefficient = coefficient?.toString();

  function returnToDepartment() {
    navigate(-1);
  }

  async function handleSubmitChange(data: FormValues) {
    if (Loading) return;
    setLoading(true);
    try {
      console.log("codeChang", _id);
      if (
        _id === undefined ||
        _title === undefined ||
        _code === undefined ||
        _coefficient === undefined
      ) {
        dispatch({
          type: "NOTIFY",
          payload: {
            type: "error",
            message: "Position code is undefined!",
          },
        });
        setLoading(false);
        document.getElementById("btn-close")?.click();
        returnToDepartment();
        return;
      }
      const res = await UpdatePosition(
        _id,
        data.title,
        data.code,
        data.coefficient
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
      {/* Open the modal using document.getElementById('ID').showModal() method */}
      <dialog
        id="change_position_modal"
        className="modal modal-bottom sm:modal-middle"
      >
        <div className="modal-box">
          <h3 className="font-bold text-lg">Change position</h3>
          <div className="flex flex-col gap-2 mt-4">
            <div className="flex gap-1 items-center justify-between">
              <label htmlFor="">Position Title: </label>
              <input
                type="text"
                placeholder="Position Title"
                className="input input-bordered w-full max-w-xs"
                defaultValue={_title}
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
                defaultValue={_code}
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
                defaultValue={_coefficient}
                {...register("coefficient")}
              />
            </div>
            {errors.coefficient && (
              <span className="text-red-600">{errors.coefficient.message}</span>
            )}
          </div>
          <div className="modal-action flex justify-center">
            <form method="dialog">
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
                  onClick={handleSubmit(handleSubmitChange)}
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

export default PositionModal;
