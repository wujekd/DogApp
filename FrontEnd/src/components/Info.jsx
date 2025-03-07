import React, { useEffect, useState } from 'react'
import { jwtDecode } from 'jwt-decode';


const Info = ({ loggedState }) => {

    useEffect(() => {
        if(loggedState) {
            const token = localStorage.getItem("auth");
            try {
            const decoded = jwtDecode(token);

            console.log(decoded)
            } catch (e) {
                console.error(e)
            }

        }
    })

  return (
    <div>{ loggedState }</div>
  )

}

export default Info