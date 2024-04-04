// interface ModalBodyProps {
//   extraObject?: any;
// }

const AddDepartModalBody = () => {
    const handleSubmit = () => {
      console.log("submit");
    };
  
    return (
      <>
        <div className="flex flex-col gap-2 mt-4">
          <div className="flex gap-1 items-center justify-between">
            <label htmlFor="">Department Name: </label>
            <input
              type="text"
              placeholder="Department name"
              className="input input-bordered w-full max-w-xs"
              // value={departmentName}
              // onChange={(e) => setDepartmentName(e.target.value)}
            />
          </div>
          <div className="flex gap-1 items-center justify-between">
            <label htmlFor="">Department Code: </label>
            <input
              type="text"
              placeholder="Department code"
              className="input input-bordered w-full max-w-xs"
              // value={departmentCode}
              // onChange={(e) => setDepartmentCode(e.target.value)}
            />
          </div>
        </div>
        <div className="modal-action flex justify-center">
          <form method="dialog">
            {/* if there is a button in form, it will close the modal */}
            <button
              className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
              // onClick={close}
            >
              ✕
            </button>
          </form>
          <form method="dialog">
            {/* if there is a button in form, it will close the modal */}
            <div className="flex justify-center gap-1">
              <button className="btn btn-error">Close</button>
              <button
                className="btn bg-tim-color text-white hover:text-black"
                onClick={handleSubmit}
              >
                {/* <span className="loading loading-infinity loading-md"></span> */}
                Submit
              </button>
            </div>
          </form>
        </div>
      </>
    );
  };
  
  export default AddDepartModalBody;
  