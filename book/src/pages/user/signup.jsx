import { useState } from 'react';
import { signupFields } from "../../models/constant"
import Input from "../../components/input";
import Header from "../../components/header";

const fields = signupFields;
let fieldsState = {};
fields.forEach(field => fieldsState[field.id] = '');

export const Signup = () => {
    const [signupState, setSignupState] = useState(fieldsState);

    const handleChange = (e) => setSignupState({ ...signupState, [e.target.id]: e.target.value });

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(signupState)
        createAccount()
    }

    // Call đăng ký api
    const createAccount = () => {

    }

    return (
        <>
            <div className='min-h-full h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8'>
                <div className='max-w-md w-full space-y-8'>
                    <Header
                    heading="Create a New Account"
                    paragraph="Already have an account? "
                    linkName="Login"
                    linkUrl="/login"
                    />
                    <form className="mt-8 space-y-6">
                        <div className="">
                            {
                                fields.map(field =>
                                    <Input
                                        key={field.id}
                                        handleChange={handleChange}
                                        value={signupState[field.id]}
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
                        <button
                            type={"submit"}
                            className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-purple-600 hover:bg-purple-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-purple-500 mt-10"
                            onSubmit={handleSubmit}
                        >

                            {"Signup"}
                        </button>
                    </form>
                </div>
            </div>
        </>
    )
}