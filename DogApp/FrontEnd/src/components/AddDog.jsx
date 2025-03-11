import React from 'react'
import { useState } from 'react';
import { addDogFields } from '../constants';
import Input from './Input';
import FormExtra from './FormExtra';
import FormAction from './FormAction';

const fields = addDogFields;
let fieldsByIds = {}
fields.map(field => fieldsByIds[field.id])

const AddDog = () => {
    const [formState, setFormState] = useState('');
  const [file, setFile] = useState(null);




  const handleChange = (e) => {
    setFormState({...formState, [e.target.id]:e.target.value})
  }

  const handleSubmit = (e)=> {
    e.preventDefault();
    console.log(formState)
  }

  return (
    <div className='mainContainer'>
        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
                <div className="-space-y-px">
                    {
                        fields.map(field=>
                                <Input
                                    key={field.id}
                                    handleChange={handleChange}
                                    value={formState[field.id]}
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
                <FormAction method="POST" text="Add Dog" />
               
        
              </form>
    </div>
  )
}

export default AddDog