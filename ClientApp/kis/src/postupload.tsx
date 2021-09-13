import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { TextField, makeStyles, Button } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
    heading: {
        textAlign: "center",
        margin: theme.spacing(1, 0, 4),
    },
    submitButton: {
        marginTop: theme.spacing(4),
    },
}));

function PostUpload(token: any){
  const [text, setText] = useState("");
  let history = useHistory();

  const { heading, submitButton } = useStyles();

  const handleChange = (event: any) => {
      setText(event.target.value);
  }

  const OnSubmit = (event : any) => {
    var submitedPost = {"Content": text, "Token": token.token};

    return fetch('https://localhost:44324/Posts/Add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(submitedPost)

    }).then(response => {

      history.push('/')

      if (!response.ok) {
        return { code: response.status, message: "Request failed error " + response.statusText };
      }
    })
  };
return <div>
        <TextField
          id="standard-textarea"
          className="heading"
          label="What's on your mind?"
          placeholder="Write.."
          value={text}
          onChange={handleChange}
          fullWidth
          multiline/>
          <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={submitButton}

              onClick={OnSubmit}

          >
              Post
          </Button>
          </div>
}

export default PostUpload;
