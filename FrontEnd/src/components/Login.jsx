import { useState } from 'react';
import { loginFields } from "../constants.js";
import Input from "./Input";
import FormExtra from './FormExtra.jsx';
import FormAction from './FormAction.jsx';

const fields=loginFields;
let fieldsState = {};
fields.forEach(field=>fieldsState[field.id]='');

export default function Login(){
    const [loginState,setLoginState]=useState(fieldsState);

    const handleChange=(e)=>{
        setLoginState({...loginState,[e.target.id]:e.target.value})
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        authenticateUser();
    }
    const authenticateUser = async ()=>{

        const {email, password } = loginState

        fetch("http://localhost:5010/Accounts/login", {
            method: "POST",
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
                "Accept": "application/json"
              },
              body: JSON.stringify({ name: email, password: password })
          })
            .then((res) => res.json())
            .then((data) => console.log("success:", data))
            .catch((err) => console.error("failed:", err));
    }

    return(
        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
        <div className="-space-y-px">
            {
                fields.map(field=>
                        <Input
                            key={field.id}
                            handleChange={handleChange}
                            value={loginState[field.id]}
                            labelText={field.labelText}
                            labelFor={field.labelFor}
                            id={field.id}
                            name={field.name}
                            type={field.type}
                            isRequired={field.isRequired}
                            placeholder={field.placeholder}
                    />
                
                )
            }
        </div>
        <FormExtra />
        <FormAction method="POST" text="Login" />
       

      </form>
    )
}