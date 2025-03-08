const loginFields=[
    {
        labelText:"Email address",
        labelFor:"email-address",
        id:"email",
        name:"email",
        type:"text",
        autoComplete:"email",
        isRequired:true,
        placeholder:"Email address"   
    },
    {
        labelText:"Password",
        labelFor:"password",
        id:"password",
        name:"password",
        type:"password",
        autoComplete:"current-password",
        isRequired:true,
        placeholder:"Password"   
    }
]

const signupFields=[
    // {
    //     labelText:"Username",
    //     labelFor:"username",
    //     id:"username",
    //     name:"username",
    //     type:"text",
    //     autoComplete:"username",
    //     isRequired:true,
    //     placeholder:"Username"   
    // },
    {
        labelText:"Email address",
        labelFor:"email-address",
        id:"email",
        name:"email",
        type:"text",
        autoComplete:"email",
        isRequired:true,
        placeholder:"Email address"   
    },
    {
        labelText:"Password",
        labelFor:"password",
        id:"password",
        name:"password",
        type:"password",
        autoComplete:"current-password",
        isRequired:true,
        placeholder:"Password"   
    },
    // {
    //     labelText:"Confirm Password",
    //     labelFor:"confirm-password",
    //     id:"confirm-password",
    //     name:"confirm-password",
    //     type:"password",
    //     autoComplete:"confirm-password",
    //     isRequired:true,
    //     placeholder:"Confirm Password"   
    // }
]



export const addDogFields = [
    {
      labelText: "Dog Name",
      labelFor: "dogName",
      id: "dogName",
      name: "dogName",
      type: "text",
      autoComplete: "off",
      isRequired: true,
      placeholder: "Enter your dog's name"
    },
    {
      labelText: "Dog Breed",
      labelFor: "dogBreed",
      id: "dogBreed",
      name: "dogBreed",
      type: "text",
      autoComplete: "off",
      isRequired: false,
      placeholder: "Optionally enter the breed"
    },
    {
      labelText: "Dog Picture",
      labelFor: "dogPicture",
      id: "dogPicture",
      name: "file",          // The key name for your server's FormData
      type: "file",
      isRequired: true,
      accept: "image/*",     // important for file inputs
      placeholder: ""
    }
  ];



const dogsData = [
    {
      id: 1,
      name: 'Buddy',
      breed: 'Golden Retriever'
    },
    {
      id: 2,
      name: 'Max',
      breed: 'German Shepherd'
    },
    {
      id: 3,
      name: 'Lucy',
      breed: 'Bulldog'
    },
    {
      id: 4,
      name: 'Molly',
      breed: 'Poodle'
    }
    // ... add more dogs as needed
  ];
  
  
export {loginFields,signupFields, dogsData}