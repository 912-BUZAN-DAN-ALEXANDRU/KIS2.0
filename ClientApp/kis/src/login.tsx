import {
    makeStyles,
    Container,
    Typography,
    TextField,
    Button,
} from "@material-ui/core";
import { useState } from "react";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as PropTypes  from 'prop-types';


interface IFormInput {
    userName: string;
    password: string;
}

const schema = yup.object().shape({
    userName: yup.string().required().min(2).max(25),
    password: yup.string().required().min(8).max(120),
});

const useStyles = makeStyles((theme) => ({
    heading: {
        textAlign: "center",
        margin: theme.spacing(1, 0, 4),
    },
    submitButton: {
        marginTop: theme.spacing(4),
    },
}));
async function loginUser(credentials: any) {
    console.log(JSON.stringify(credentials));
    return fetch('https://localhost:44324/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    }).then(response => {
            console.log(response)
            if (!response.ok) {
                if (response.status == 401) {
                    return { code: 401, message: "Username or password is incorrect" };
                }
                else {
                    return { code: response.status, message: "Request failed error " + response.statusText };
                }
            }

            const data = response.json();
            console.log(data);
            return data;
        })
}

function LogIn({ setToken } : {setToken:any}) {

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<IFormInput>({
        resolver: yupResolver(schema),
    });

    const { heading, submitButton } = useStyles();
    const [error, setError] = useState("");


    const onSubmit = async (data: IFormInput) => {
        console.log(data);
        let loginInfo = {
            Username: data.userName,
            Password: data.password

        }
        const response = await loginUser(loginInfo);
        if (typeof response.token === "undefined") {
            setError(response.message)
        }
        else {
            setToken(response.token);
        }

    }

    return (
        <Container maxWidth="xs">
            <Typography className={heading} variant="h3">
                Log In
            </Typography>
            <form onSubmit={handleSubmit(onSubmit)} noValidate>

                <TextField
                    {...register("userName")}
                    variant="outlined"
                    margin="normal"
                    label="Username"

                    fullWidth
                    required
                />
                <TextField
                    {...register("password")}
                    variant="outlined"
                    margin="normal"
                    label="Password"
                    type="password"

                    fullWidth
                    required
                />


                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    color="primary"
                    className={submitButton}
                >
                    Log In

                </Button>
                {error}
            </form>
        </Container>
    );
}

LogIn.propTypes = {
    setToken: PropTypes.func.isRequired
}

export default LogIn;
