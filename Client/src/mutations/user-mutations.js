import { gql } from "@apollo/client"
import client from "../client/apollo-client"

const registerUserMutation = async (userInput) => {

    const REGISTER_USER_MUTATION = gql`mutation createUser {
        createUser(userInput: {
            firstName: "${userInput.firstName}",
            lastName: "${userInput.lastName}",
            email: "${userInput.email}",
            password: "${userInput.password}"
        }){
            boolean
        }
      }`

      var { data } = await client.mutate({
        mutation: REGISTER_USER_MUTATION
    });

    return data
}

const loginUserMutation = async (userInput) => {

    const LOGIN_USER_MUTATION = gql`mutation loginUser {
        loginUser(userInput: {
            email: "${userInput.email}"
            password: "${userInput.password}"
        }) {
          loginUserOutput {
            firstName
            lastName
            email
            token
          }
        }
      }`
    
    try {
        var { data } = await client.mutate({
            mutation: LOGIN_USER_MUTATION,
        });
    } catch (err) {
        console.log(err)
    }

    return data.loginUser.loginUserOutput
}


export {
    registerUserMutation,
    loginUserMutation
}