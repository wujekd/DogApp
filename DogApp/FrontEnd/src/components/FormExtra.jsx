export default function FormExtra(){
    return(
        <div className="flex items-center justify-between ">
        <div className="flex items-center">
          <input
            id="remember-me"
            name="remember-me"
            type="checkbox"
            className="h-4 w-4 text-accent focus:ring-purple-500 border-gray-300 rounded"
          />
          <label htmlFor="remember-me" className="ml-2 block text-sm">
            I accept the policy
          </label>
        </div>

        <div className="text-sm">
          <a href="#" className="font-medium text-accent hover:text-purple-500">
            Forgot your password? Thats your problem mate.
          </a>
        </div>
      </div>

    )
}