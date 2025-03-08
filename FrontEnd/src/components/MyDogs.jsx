import React from 'react'
import { dogsData } from '../constants'
const MyDogs = () => {
    console.log(dogsData)
  return (
    <div className='mainContainer'>
    {dogsData.map((dog, key) => {
        console.log(dog)
        return (
        <div className='dog' key={key}>
            <div className='dogItemPic bg-fuchsia-300 w-[320px] h-[280px]'>d</div>
            <p className='p-4'>{ dog.name }</p>
            <p>breed: { dog.breed }</p>

        </div>
        )
    })}
    </div>
  
)}
export default MyDogs