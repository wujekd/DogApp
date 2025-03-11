import React from 'react'
import Signup from '../components/Signup'
import Header from '../components/Header'


const SignupPage = () => {
  return (
    <div className="bg-primary-10 p-4">
        dfgdfg
        <Header
            heading="signup"
            paragraph="Already have an account?"
            linkName="Login"
            linkUrl="/"
            />
        <Signup />
    </div>
  )
}


export default SignupPage