import { RegistrationForm, SignInForm } from "../../components";
import './authentication.css'

const Authentication = () => {
    const login = window.location.pathname.endsWith('login');
    
    return (
        <div className="form-container">
            {login
                ? <SignInForm />
                : <RegistrationForm />
            }
        </div>
    )
}

export default Authentication