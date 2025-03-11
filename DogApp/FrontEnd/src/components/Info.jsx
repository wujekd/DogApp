import React, { useEffect, useState } from 'react'
import { jwtDecode } from 'jwt-decode';


const Info = ({ loggedState, setLoggedState }) => {

    const [userName, setUserName] = useState(null)

    useEffect(() => {
        if(loggedState) {
            const token = localStorage.getItem("auth");
            try {
            const decoded = jwtDecode(token);
            setUserName(decoded.UserName);

            } catch (e) {
                console.error(e)
            }
        } else {
            setUserName("")
        }
    })

    const handleLogout = () => {
        localStorage.setItem("auth", null)
        setLoggedState(false)
    }

  return (
    <div className='fixed top-8 flex'>
        <div>logged user: { userName }</div>
        <button onClick={ ()=> { handleLogout() }} className='bg-red-300 m-4 px-4 py-1'>logout</button>
    </div>
  )

}

export default Info