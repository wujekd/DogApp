import Header from "../components/Header"
import Login from "../components/Login"

export default function LoginPage({setIsAuthenticated}){

    const makeReq = async ()=> {
        fetch("http://localhost:5010/Accounts/login", {
            method: "POST",
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
                "Accept": "application/json"
              },
              body: JSON.stringify({ name: "string", password: "string" })
          })
            .then((res) => res.json())
            .then((data) => console.log("CORS test success:", data))
            .catch((err) => console.error("CORS test failed:", err));
    }

    return(
        <div className="bg-primary-10 p-4">
            <Header
                heading="Login to your account"
                paragraph="Don't have an account yet? "
                linkName="Signup"
                linkUrl="/signup"
                />
            <Login />
            <button onClick={makeReq}>fetch</button>
            
        </div>
    )
}