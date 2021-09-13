import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import clsx from "clsx";
import Card from "@material-ui/core/Card";
import Grid from "@material-ui/core/Grid";

import CardHeader from "@material-ui/core/CardHeader";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import Collapse from "@material-ui/core/Collapse";
import IconButton from "@material-ui/core/IconButton";
import Typography from "@material-ui/core/Typography";
import { red } from "@material-ui/core/colors";
import FavoriteIcon from "@material-ui/icons/Favorite";
import ThumbUp from "@material-ui/icons/ThumbUp";
import ThumbDown from "@material-ui/icons/ThumbDown";
import Comment from "@material-ui/icons/Comment";
import Send from "@material-ui/icons/Send";

import TextField from "@material-ui/core/TextField";

const useStyles = makeStyles((theme) => ({
  root: {
    maxWidth: 345
  },
  media: {
    height: 0,
    paddingTop: "56.25%" // 16:9
  },
  commentButton: {
    transform: "rotate(0deg)",
    marginLeft: "auto",
    transition: theme.transitions.create("transform", {
      duration: theme.transitions.duration.shortest
    })
  },

  avatar: {
    backgroundColor: red[500]
  }
}));

export default function PostCard(jsonPost: any) {
  const classes = useStyles();
  const [expanded, setExpanded] = React.useState(false);
console.log(jsonPost);
var post = jsonPost.jsonPost;
  const handleExpandClick = () => {
    setExpanded(!expanded);
  };
  return (
    <Card className={classes.root}>
      <CardHeader
        title={post.username}
        subheader={post.date}
      />
      <CardContent>
        <Typography variant="body2" color="textSecondary" component="p">
          {post.content}
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
        <IconButton aria-label="like">
          <ThumbUp />
        </IconButton>
        <IconButton aria-label="love">
          <FavoriteIcon />
        </IconButton>
        <IconButton aria-label="dislike">
          <ThumbDown />
        </IconButton>
        <IconButton
          className={clsx(classes.commentButton)}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="show more"
        >
          <Comment />
        </IconButton>
      </CardActions>

      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Grid container spacing={1}>
            <Grid item xs={10}>
              <TextField id="standard-basic" label="Write.." fullWidth />
            </Grid>
            <Grid item xs={2}>
              <IconButton aria-label="dislike">
                <Send />
              </IconButton>
            </Grid>
          </Grid>
        </CardContent>
      </Collapse>
    </Card>
  );
}
