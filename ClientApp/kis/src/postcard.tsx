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
  },

  commentHeader: {
//    fullWidth,
  }
}));

export default function PostCard(param: any) {
  const classes = useStyles();
  const [reactions, updateReactions] = React.useState([] as any[]);
  const [comments, updateComments] = React.useState([] as any[]);
  const [expanded, setExpanded] = React.useState(false);
  const [draftComment, setDraftComment] = React.useState("");
  const [liked, setLiked] = React.useState(false);
  const [loved, setLoved] = React.useState(false);
  const [disliked, setDisliked] = React.useState(false);

  var post = param.param.post;
  var token = param.param.token;

  React.useEffect(function effectFunction() {
    update();
  });

  const update = async () => {
    await fetch('https://localhost:44324/' + post.id + '/Reactions', {method: 'GET'})
                .then(response => response.json())
                .then(response =>updateReactions(response));
    await fetch('https://localhost:44324/' + post.id + '/Comments', {method: 'GET'})
                            .then(response => response.json())
                            .then(response =>updateComments(response));
  };

  const handleExpandClick = () => {
    console.log(comments);
    setExpanded(!expanded);
  };



  const handleLikeButton =() => {
      var reaction = { "PostId":post.id, "ReactionType":0, "Token": token.token};
      return fetch('https://localhost:44324/Reactions/Add', {method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(reaction)
                  }).then(response => {
                                      if (!response.ok) {
                                          return { code: response.status, message: "Request failed error " + response.statusText };
                                      }
                        update();
                        if (liked){
                          setLiked(false);
                        }
                        else {
                          setLiked(true);
                        }

                        setLoved(false);
                        setDisliked(false);
                      });
  }

  const handleLoveButton =() => {
    var reaction = {"PostId":post.id, "ReactionType":1, "Token": token.token};
    return fetch('https://localhost:44324/Reactions/Add', {method: 'POST',
                  headers: {
                      'Content-Type': 'application/json'
                  },
                  body: JSON.stringify(reaction)
                }).then(response => {
                                    if (!response.ok) {
                                        return { code: response.status, message: "Request failed error " + response.statusText };
                                    }
                                    update();
                                    if (loved){
                                      setLoved(false);
                                    }
                                    else {
                                      setLoved(true);
                                    }

                                    setLiked(false);
                                    setDisliked(false);

                    });
  }

  const handleDislikeButton =() => {
    var reaction = { "PostId":post.id, "ReactionType":2, "Token": token.token};
    return fetch('https://localhost:44324/Reactions/Add', {method: 'POST',
                  headers: {
                      'Content-Type': 'application/json'
                  },
                  body: JSON.stringify(reaction)
                }).then(response => {
                                    if (!response.ok) {
                                        return { code: response.status, message: "Request failed error " + response.statusText };
                                    }
                                    update();
                                    if (disliked){
                                      setDisliked(false);
                                    }
                                    else {
                                      setDisliked(true);
                                    }

                                    setLoved(false);
                                    setLiked(false);
                    });
  }

  const sendComment = () => {
      var comment = {"PostId": post.id, "Content": draftComment, "Token": token.token};
      return fetch('https://localhost:44324/Comments/Add', {method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(comment)
                  }).then(response => {
                                      if (!response.ok) {
                                          return { code: response.status, message: "Request failed error " + response.statusText };
                                      }
                                      update();
                                    }
                                  );
  }
   const handleChange = (event: any) => {
     setDraftComment(event.target.value);
   }
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
        <IconButton aria-label="like" onClick={handleLikeButton} color={liked ? 'primary' : 'default'}>
          <ThumbUp />
        </IconButton>
      <span>{reactions.filter(r => r.reactionType == 0).length}</span>
        <IconButton aria-label="love" onClick={handleLoveButton} color={loved ? 'secondary' : 'default'}>
          <FavoriteIcon />
        </IconButton>
          <span>{reactions.filter(r => r.reactionType == 1).length}</span>

        <IconButton aria-label="dislike" onClick={handleDislikeButton} color={disliked ? 'primary' : 'default'}>
          <ThumbDown />
        </IconButton>
          <span>{ reactions.filter(r => r.reactionType == 2).length}</span>

        <IconButton
          className={clsx(classes.commentButton)}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="show more"
        >
          <Comment />
        </IconButton>
      </CardActions>
      <CardContent>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        {comments.map(comm => <Card className={classes.commentHeader}>
                                  <CardHeader
                                      subheader={comm.username}
                                  />
                                <CardContent>
                                  <Typography variant="body2" color="textSecondary" component="p">
                                    {comm.content}
                                  </Typography>
                                </CardContent>
                              </Card>
        )}

          <Grid container spacing={1}>
            <Grid item xs={10}>
              <TextField id="standard-basic" label="Write.." onChange={handleChange} onClick={sendComment} fullWidth />
            </Grid>
            <Grid item xs={2}>
              <IconButton aria-label="dislike" onClick={sendComment}>
                <Send />
              </IconButton>
            </Grid>
          </Grid>
          </Collapse>
        </CardContent>
    </Card>
  );
}
