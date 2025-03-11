import React, { useState } from 'react'
import { signupFields } from '../constants';
import Input from './Input';
import FormAction from './FormAction';

const fields=signupFields;
let fieldsState = {};
fields.forEach(field=>fieldsState[field.id]='');

const Signup = () => {

    const [inputStates, setInputStates] = useState(fieldsState)
    const handleChange=(e)=>setInputStates({...inputStates,[e.target.id]:e.target.value});
    const [resultMessage, setResultMessage] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e)=> {
        e.preventDefault();
        createAccount();
    }

    const createAccount = async ()=> {
         const { email, password } = inputStates;
         console.log("name:", email, " password: ", password)

         fetch("http://localhost:5010/Accounts/register", {
            method: "POST",
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
                "Accept": "application/json"
              },
              body: JSON.stringify({ name: email, password: password })
          })
            .then((res) => res.json())
            .then((data) => {
                console.log("success:", data)
                setResultMessage(data.message)

            })
            .catch((err) => console.error("failed:", err));
    }


  return (
    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
        <div className=''>
        {
                fields.map(field=>
                        <Input
                            key={field.id}
                            handleChange={handleChange}
                            value={inputStates[field.id]}
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
          <FormAction method="POST" text="Signup" />
        </div>
        {resultMessage && <p style={{ color: 'green' }}>{resultMessage}</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}


    </form>
  )
}

export default Signup