import Header from "../components/Header"
import Login from "../components/Login"

export default function LoginPage({setLoggedState}){

    return(
        <div className="bg-primary-10 p-4">
            <Header
                heading="Login to your account"
                paragraph="Don't have an account yet? "
                linkName="Signup"
                linkUrl="/signup"
                />
            <Login setLoggedState={setLoggedState} />
            
        </div>
    )
}