import { Link } from "react-router-dom";

const Error = () => {
  return (
    <div className="min-h-screen bg-gray-100 flex flex-col justify-center sm:px-6 lg:px-4">
      <div className="mt-2 sm:mx-auto sm:w-full sm:max-w-md">
        <div className="bg-white py-8 px-4 shadow sm:rounded-lg sm:px-10">
          <div className="sm:mx-auto sm:w-full sm:max-w-md">
            <div className="text-center">
              <h2 className="text-3xl font-bold text-do-color">404 Error</h2>
              <p className="mt-2 text-lg font-medium text-gray-900">
                Page not found
              </p>
              <p className="mt-1 text-gray-600">
                The page you are looking for does not exist.
              </p>
            </div>
            <div className="mt-6">
              <Link
                to={"/"}
                className="w-full flex items-center justify-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm no-underline font-medium text-white bg-do-color hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
              >
                Go back home
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Error;
