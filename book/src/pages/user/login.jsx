import React from 'react';
import { GoogleLogo, FacebookLogo } from "phosphor-react";
import Header from "../../components/header";
import { useState } from 'react';
import Input from "../../components/input";
import { loginFields } from "../../models/constant";

import {useNavigate} from 'react-router-dom';

const fields = loginFields;
let fieldsState = {};
fields.forEach(field => fieldsState[field.id] = '');

export const Login = () => {
    const [loginState, setLoginState] = useState(fieldsState);

    const handleChange = (e) => {
        setLoginState({ ...loginState, [e.target.id]: e.target.value })
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        authenticateUser();
    }

    const navigate = useNavigate();
    // Call đăng nhập api
    const authenticateUser = () => {
    }
    return (
        <>
            <Header
                heading="Login to your account"
                paragraph="Don't have an account yet? "
                linkName="Signup"
                linkUrl="/signup"
            />
            <form className="mt-8 space-y-6">
                <div className="-space-y-px">
                    {
                        fields.map(field =>
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
                <div className="flex items-center justify-between ">
                    <div className="flex items-center">
                        <input
                            id="remember-me"
                            name="remember-me"
                            type="checkbox"
                            className="h-4 w-4 text-purple-600 focus:ring-purple-500 border-gray-300 rounded"
                        />
                        <label htmlFor="remember-me" className="ml-2 block text-sm text-gray-900">
                            Remember me
                        </label>
                    </div>

                    <div className="text-sm">
                        <a href="#" className="font-medium text-purple-600 hover:text-purple-500">
                            Forgot your password?
                        </a>
                    </div>
                </div>
                <button
                    type={"submit"}
                    className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-purple-600 hover:bg-purple-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-purple-500 mt-10"
                    //onSubmit={handleSubmit}
                    onClick={()=> navigate('/')}
                >

                    {"Login"}
                </button>
            </form>
        </>
    );
};
