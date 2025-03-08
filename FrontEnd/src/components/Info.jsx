import React, { useEffect, useState } from 'react'
import { jwtDecode } from 'jwt-decode';


const Info = ({ loggedState }) => {

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
        }
    })

  return (
    <div className='fixed top-8'>logged user: { userName }</div>
  )

}

export default Info