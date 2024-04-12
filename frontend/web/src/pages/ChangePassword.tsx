import React from "react";

const ChangePassword: React.FC = () => {
  return (
    <div>
      <h1 className="font-bold text-2xl text-gray-900 mt-8">Reset Password</h1>
      <section className="grid grid-cols-3 grid-rows-3">
        <button
          type="submit"
          className="btn bg-tim-color text-white hover:text-black mt-4 w-full col-start-2 row-start-2"
        >
          Reset Password
        </button>
      </section>
    </div>
  );
};

export default ChangePassword;
