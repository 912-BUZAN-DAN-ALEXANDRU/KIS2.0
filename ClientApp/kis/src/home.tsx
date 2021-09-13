
import * as React from 'react';
import {makeStyles} from  "@material-ui/core";
import * as ReactDOM from 'react-dom';
import PostCard from './postcard';
import Grid from '@material-ui/core/Grid';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  }
}));

function Home() {
  const classes = useStyles();

  const [posts, updatePosts] = React.useState([]);

  React.useEffect(function effectFunction() {
    fetch('https://localhost:44324/Posts',{method: 'GET',})
          .then(response => response.json())
          .then(response => updatePosts(response));
  }, []);

  return <div className={classes.root}>
            <Grid container spacing={3}>
            {posts.map(post => <Grid item xs><PostCard jsonPost={post} /></Grid>)}
            </Grid>
         </div>
}
export default Home;
