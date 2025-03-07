import React from 'react'
import MyDogs from '../components/MyDogs'
import AddDog from '../components/AddDog'


const Main = () => {
  return (
    <div className='flex w-full'>
      
      <MyDogs />
      <AddDog />
    </div>
  )
}


export default Main